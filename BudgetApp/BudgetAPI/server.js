require("dotenv").config();
const express = require("express");
const mongoose = require("mongoose");
const cors = require("cors");
const app = express();
const User = require("./models/User");
const Company = require("./models/Company");
const jwt = require("jsonwebtoken");
const bcrypt = require("bcryptjs");

// Middleware
app.use(cors());
app.use(express.json());

// Połączenie z MongoDB
mongoose
	.connect(process.env.MONGODB_URI)
	.then(() => console.log("Połączono z MongoDB"))
	.catch((err) => console.error("Błąd połączenia:", err));

// Test endpoint
app.get("/api/test", (req, res) => {
	res.json({ message: "API działa!" });
});

// Endpoint do pobierania zobowiązań podatkowych (NAPRAWIONE)
app.get("/api/taxes", async (req, res) => {
	try {
		const taxes = [
			{ name: "Podatek dochodowy", amount: 1500, dueDate: "2025-03-15" },
			{ name: "VAT", amount: 800, dueDate: "2025-03-20" },
		];
		res.json(taxes);
	} catch (error) {
		console.error("Błąd pobierania zobowiązań podatkowych:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Rejestracja
app.post("/api/auth/register", async (req, res) => {
	try {
		const { username, email, password, companyName, companyAddress } =
			req.body;

		if (!username || !email || !password || !companyName) {
			return res
				.status(400)
				.json({ message: "Wypełnij wszystkie wymagane pola" });
		}

		const existingUser = await User.findOne({
			$or: [{ username }, { email }],
		});
		if (existingUser) {
			return res.status(400).json({
				message: "Użytkownik o podanej nazwie lub emailu już istnieje",
			});
		}

		const salt = await bcrypt.genSalt(10);
		const passwordHash = await bcrypt.hash(password, salt);

		// Tworzenie użytkownika
		const newUser = new User({
			username,
			email,
			passwordHash,
		});
		await newUser.save();

		// Tworzenie firmy przypisanej do użytkownika
		const newCompany = new Company({
			name: companyName,
			address: companyAddress,
			owner: newUser._id,
		});
		await newCompany.save();

		// Przypisanie firmy do użytkownika
		newUser.companies.push(newCompany._id);
		await newUser.save();

		// Generowanie tokena JWT
		const token = jwt.sign(
			{ userId: newUser._id },
			process.env.JWT_SECRET,
			{ expiresIn: "1h" }
		);

		res.status(201).json({
			token,
			userId: newUser._id,
			companyId: newCompany._id,
		});
	} catch (error) {
		console.error("Błąd rejestracji:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Logowanie
app.post("/api/auth/login", async (req, res) => {
	try {
		const { username, password } = req.body;

		console.log("Otrzymane dane:", username, password); // Debug

		const user = await User.findOne({ username: username.toLowerCase() });
		if (!user) {
			return res
				.status(400)
				.json({ message: "Nieprawidłowa nazwa użytkownika" });
		}

		console.log("Znaleziony użytkownik:", user);

		const isPasswordValid = await bcrypt.compare(
			password,
			user.passwordHash
		);
		if (!isPasswordValid) {
			return res.status(400).json({ message: "Nieprawidłowe hasło" });
		}

		const token = jwt.sign({ userId: user._id }, process.env.JWT_SECRET, {
			expiresIn: "1h",
		});

		res.json({ token, userId: user._id });
	} catch (error) {
		console.error("Błąd serwera:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}

	app.put("/api/taxes/:id", async (req, res) => {
		try {
			const { id } = req.params;
			const { name, amount, dueDate } = req.body;

			// Znajdź podatek w bazie danych (jeśli używasz bazy, tu dodaj model)
			console.log(
				`Aktualizacja podatku ${id}: ${name}, ${amount}, ${dueDate}`
			);

			res.json({ message: "Podatek zaktualizowany" });
		} catch (error) {
			console.error("Błąd aktualizacji podatku:", error);
			res.status(500).json({ message: "Błąd serwera" });
		}
	});
});

// Uruchomienie serwera
const PORT = process.env.PORT || 5000;
app.listen(PORT, () => {
	console.log(`Serwer działa na porcie ${PORT}`);
});
