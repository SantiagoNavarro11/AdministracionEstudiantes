document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    // Obtener los datos de los controles (input) HTML.
    const correo = document.getElementById("correo").value;
    const contrasena = document.getElementById("contrasena").value;

    // Mandar a encriptar la contraseña.
    encriptarSHA256(contrasena).then(contrasenaEncriptada => {

        // Cuando la contraseña ya esta encriptada, entonces.

        // 1. Armar objeto que se envia al API.
        let autenticacion = { correo, contrasena: contrasenaEncriptada };

        // 2. Consimor API.
        fetch("https://localhost:7225/api/usuarios/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(autenticacion)
        })
            .then(response => response.json())
            .then(resultado => {

                console.log("Respuesta de la API:", resultado);

                if (resultado.exito == true) {                    
                    alertaConfirmacion(resultado.mensaje);
                    localStorage.clear();
                    localStorage.setItem("usuarioactual", JSON.stringify(resultado.datos));
                    document.getElementById("loginForm").reset();
                    window.location.href = "principal.html";
                }
                else {
                    alertaAdvertencia(resultado.mensaje);
                }
            })
            .catch(error => {
                alertaError(error);
                console.error("Error al registrar usuario:", error)
            });
    });
});
