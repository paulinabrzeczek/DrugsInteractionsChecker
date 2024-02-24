document.querySelector("form").addEventListener("submit", (e) => {
    e.preventDefault();
    fetch('/User/LoginUser', {
        method: 'POST',
        body: new FormData(document.querySelector("form"))
    })
        .then(response => {
            if (response.redirected) {
                window.location.href = response.url;
            } else {
                return response.json();
            }
        })
        .then(data => {
            if (data.message) {
                const errorMessage = document.createElement('div');
                errorMessage.textContent = data.message;
                document.body.appendChild(errorMessage);
            }
        })
        .catch(error => {
            console.error('Wystąpił błąd:', error);
        });

    document.querySelector("form").reset();
});
