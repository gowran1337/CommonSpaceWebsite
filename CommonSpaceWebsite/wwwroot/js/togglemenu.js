function togglePopup() {
	const form = document.getElementById("formContainer");
	const footer = document.getElementById("footercleaning");
	const button = document.getElementById("popupToggleBtn");




	if (form.classList.contains("show")) {
		// Close the menu
		form.classList.remove("show");
		footer.classList.remove("moved");
		button.textContent = "Add Task";
	} else {

		form.classList.add("show");
		footer.classList.add("moved");
		button.textContent = "Close";
	}
}