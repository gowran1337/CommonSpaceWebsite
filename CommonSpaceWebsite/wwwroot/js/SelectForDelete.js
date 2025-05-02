document.addEventListener("DOMContentLoaded", function () {
    const shoppingItems = document.querySelectorAll(".selectable-item"); //hämtar listan
    const cleaningTasks = document.querySelectorAll(".selectable-task"); //hämtar listan


    const hiddenInput = document.getElementById("selectedItemId"); //hämtar den valda itemet osparar den temporärt o skickar sen till .cs
    const hiddenTypeInput = document.getElementById("selectedItemType"); //hämtar den valda itemet
    const deleteButton = document.getElementById("deleteBTN");


    function clearSelections() { //funktion som resetar valet
        shoppingItems.forEach(item => item.classList.remove("selected"));
        cleaningTasks.forEach(task => task.classList.remove("selected"));
    }

    shoppingItems.forEach((item) => { //loopar igenom shoppingItems
        item.addEventListener("click", function () { //ge varje item en eventlistener
            clearSelections();
            item.classList.add("selected"); //ger itemet en class för css

            hiddenInput.value = item.dataset.id; //ger itemet en id som är kopplad till html 
            hiddenTypeInput.value = "shoppingItem";//tilldelar den stringet "shoppingItem" så det kkan användas i .cs
            deleteButton.disabled = false;
        });
    });

    cleaningTasks.forEach((task) => {
        task.addEventListener("click", function () {
            clearSelections();
            task.classList.add("selected");

            hiddenInput.value = task.dataset.id;
            hiddenTypeInput.value = "task";
            deleteButton.disabled = false;
        });
    });
});
 
   