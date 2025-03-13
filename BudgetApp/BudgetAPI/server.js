require("dotenv").config();
const express = require("express");
const mongoose = require("mongoose");
const cors = require("cors");
const app = express();
const User = require("./models/User");
const Company = require("./models/Company");
const Transaction = require("./models/Transaction");
const TaxObligation = require("./models/TaxObligation");
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

		const newUser = new User({ username, email, passwordHash });
		await newUser.save();

		const newCompany = new Company({
			name: companyName,
			address: companyAddress,
			owner: newUser._id,
		});
		await newCompany.save();

		newUser.companies.push(newCompany._id);
		await newUser.save();

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

		const user = await User.findOne({ username: username.toLowerCase() });
		if (!user) {
			return res
				.status(400)
				.json({ message: "Nieprawidłowa nazwa użytkownika" });
		}

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
		console.error("Błąd logowania:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Endpoint do dodawania transakcji
app.post("/api/transactions/add", async (req, res) => {
	try {
		const { name, amount, date, companyId } = req.body;

		if (!name || !amount || !date || !companyId) {
			return res
				.status(400)
				.json({ message: "Wypełnij wszystkie wymagane pola" });
		}

		const newTransaction = new Transaction({
			name,
			amount,
			date,
			companyId,
		});
		await newTransaction.save();

		res.status(201).json({
			message: "Transakcja dodana pomyślnie",
			transaction: newTransaction,
		});
	} catch (error) {
		console.error("Błąd dodawania transakcji:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Endpoint do pobierania transakcji
app.get("/api/transactions", async (req, res) => {
	try {
		const { companyId } = req.query;
		const transactions = await Transaction.find({ companyId });
		res.json(transactions);
	} catch (error) {
		console.error("Błąd pobierania transakcji:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Endpoint do dodawania podatku
app.post("/api/taxes/add", async (req, res) => {
	try {
		const { name, amount, dueDate, companyId } = req.body;

		if (!name || !amount || !dueDate || !companyId) {
			return res
				.status(400)
				.json({ message: "Wypełnij wszystkie wymagane pola" });
		}

		const newTax = new TaxObligation({ name, amount, dueDate, companyId });
		await newTax.save();

		res.status(201).json({
			message: "Podatek dodany pomyślnie",
			tax: newTax,
		});
	} catch (error) {
		console.error("Błąd dodawania podatku:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Endpoint do pobierania podatków
app.get("/api/taxes", async (req, res) => {
	try {
		const { companyId } = req.query;
		const taxes = await TaxObligation.find({ companyId });
		res.json(taxes);
	} catch (error) {
		console.error("Błąd pobierania podatków:", error);
		res.status(500).json({ message: "Błąd serwera" });
	}
});

// Uruchomienie serwera
const PORT = process.env.PORT || 5000;
app.listen(PORT, () => {
	console.log(`Serwer działa na porcie ${PORT}`);
});
