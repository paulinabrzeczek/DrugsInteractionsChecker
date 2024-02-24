
    // Funkcja do zapisania wyboru w localStorage
    function zapiszWybor1() {
        var selectElement1 = document.getElementById("MySelect1");
        var wybranaOpcja1 = selectElement1.value;
        localStorage.setItem("wybor1", wybranaOpcja1);
    }

    function zapiszWybor2() {
        var selectElement2 = document.getElementById("MySelect2");
        var wybranaOpcja2 = selectElement2.value;
        localStorage.setItem("wybor2", wybranaOpcja2);
    }

    // Funkcja do odczytania wyboru z localStorage
    function odczytajWybor() {
        var wybor1 = localStorage.getItem("wybor1");
        var wybor2 = localStorage.getItem("wybor2");
        if (wybor1) {
            var selectElement1 = document.getElementById("MySelect1");
            selectElement1.value = wybor1;
        }
        if (wybor2) {
            var selectElement2 = document.getElementById("MySelect2");
            selectElement2.value = wybor2;
        }
    }
// Wywołanie funkcję odczytajWybor() przy załadowaniu strony
    odczytajWybor();

// Nasłuchiwanie zdarzenia zmiany wyboru i zapisuj go w localStorage
    var selectElement1 = document.getElementById("MySelect1");
    selectElement1.addEventListener("change", zapiszWybor1);

    var selectElement2 = document.getElementById("MySelect2");
    selectElement2.addEventListener("change", zapiszWybor2);