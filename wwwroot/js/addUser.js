document.querySelector("form").addEventListener("submit", (e) => {
    e.preventDefault();

    // Pobierz wartości pól
    //const username = document.getElementById("Username").value;
    const email = document.getElementById("Email").value;
    const password = document.getElementById("Passwordhash").value;

    // Wykonaj walidację
    const validationMessage = validate(email, password);

    if (validationMessage) {
        // Jeśli są błędy walidacji, wyświetl komunikat
        const errorMessage = document.createElement('div');
        errorMessage.textContent = validationMessage;
        document.body.appendChild(errorMessage);
    } else {
        // Jeśli walidacja przebiegła pomyślnie, wyslij formularz
        const formData = new FormData(document.querySelector("form"));
        fetch('/User/AddUser', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.message) {
                    const errorMessage = document.createElement('div');
                    errorMessage.textContent = data.message;
                    document.body.appendChild(errorMessage);
                } else if (data.redirectToUrl) {
                    window.location.href = data.redirectToUrl;
                }
            })
            .catch(error => {
                console.error('Wystąpił błąd:', error);
            });

        document.querySelector("form").reset();
    }
});

function validate(email, password) {
    // Dodaj własną logikę walidacji
    let validationMessage = '';

    if (!email) {
        validationMessage += 'Adres email jest wymagany.\n';
    }

    if (!password) {
        validationMessage += 'Hasło jest wymagane.\n';
    } else if (password.length < 8) {
        validationMessage += 'Hasło musi mieć przynajmniej 8 znaków.\n';
    }

    return validationMessage;
}
