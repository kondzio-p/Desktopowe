const mongoose = require("mongoose");
const bcrypt = require("bcryptjs");

const userSchema = new mongoose.Schema({
	username: { type: String, unique: true, required: true },
	email: { type: String, unique: true, required: true },
	passwordHash: { type: String, required: true },
	companies: [{ type: mongoose.Schema.Types.ObjectId, ref: "Company" }],
	createdAt: { type: Date, default: Date.now },
});

userSchema.methods.isValidPassword = async function (password) {
	return await bcrypt.compare(password, this.passwordHash);
};

module.exports = mongoose.model("User", userSchema);
