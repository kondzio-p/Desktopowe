const mongoose = require("mongoose");

const companySchema = new mongoose.Schema({
	name: { type: String, required: true },
	address: { type: String, required: true },
	owner: { type: mongoose.Schema.Types.ObjectId, ref: "User" }, // Powiązanie z użytkownikiem
	createdAt: { type: Date, default: Date.now },
});

module.exports = mongoose.model("Company", companySchema);
