let activePopup = null; // Track which popup is currently active

function closeAllPopups() {
    document.getElementById("registerForm").classList.remove("show");
    document.getElementById("loginForm").classList.remove("show");
    document.getElementById("registerBTN").classList.remove("active");
    document.getElementById("loginBTN").classList.remove("active");
    activePopup = null;
}

function toggleRegisterPopUp() {
    const form = document.getElementById("registerForm");

    // If this is already the active popup, close it
    if (activePopup === "register") {
        closeAllPopups();
        return;
    }

    // Otherwise, close all and open this one
    closeAllPopups();
    form.classList.add("show");
    document.getElementById("registerBTN").classList.add("active");
    activePopup = "register";
}

function toggleLoginPopUp() {
    const form = document.getElementById("loginForm");

    // If this is already the active popup, close it
    if (activePopup === "login") {
        closeAllPopups();
        return;
    }

    // Otherwise, close all and open this one
    closeAllPopups();
    form.classList.add("show");
    document.getElementById("loginBTN").classList.add("active");
    activePopup = "login";
}