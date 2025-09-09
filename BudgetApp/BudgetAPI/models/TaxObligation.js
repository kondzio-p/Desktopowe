const mongoose = require("mongoose");

const taxObligationSchema = new mongoose.Schema({
	name: { type: String, required: true },
	amount: { type: Number, required: true },
	dueDate: { type: Date, required: true },
});

module.exports = mongoose.model("TaxObligation", taxObligationSchema);
