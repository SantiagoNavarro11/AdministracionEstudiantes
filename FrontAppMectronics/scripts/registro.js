document.addEventListener("DOMContentLoaded", function () {
    cargarRoles(); // Cargar roles cuando la página se carga

    document.getElementById("registroForm").addEventListener("submit", function (event) {
        event.preventDefault();
        registrarUsuario();
    });
});

function cargarRoles() {
    fetch("https://localhost:44386/api/Rol")  // Asegúrate de que el puerto es correcto
        .then(response => response.json())
        .then(data => {
            console.log("Roles recibidos:", data);

            // Verificar si la respuesta contiene la propiedad "datos" o es directamente el array
            let rolesArray = Array.isArray(data) ? data : data.datos;
            if (!rolesArray || !Array.isArray(rolesArray)) {
                console.error("No se recibió un array de roles.");
                return;
            }

            const selectRoles = document.getElementById("IdRoles"); // Asegúrate de que el ID es correcto
            selectRoles.innerHTML = ""; // Limpiar opciones previas

            // Agregar opción por defecto
            let defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Seleccione un rol";
            defaultOption.disabled = true;
            defaultOption.selected = true;
            selectRoles.appendChild(defaultOption);

            // Agregar los roles al select
            rolesArray.forEach(rol => {
                let option = document.createElement("option");
                option.value = rol.idRol;
                option.textContent = rol.nombreRol;
                selectRoles.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar roles:", error));
}

function registrarUsuario() {
    let usuario = {
        nombres: document.getElementById("nombres").value,
        apellidos: document.getElementById("apellidos").value,
        edad: parseInt(document.getElementById("edad").value),
        correoElectronico: document.getElementById("correo").value,
        contrasena: document.getElementById("contrasena").value,
        idRoles: parseInt(document.getElementById("IdRoles").value), // ID del rol seleccionado
        fecha: new Date().toISOString().split("T")[0] // Fecha actual en formato YYYY-MM-DD
    };

    console.log("Usuario a registrar:", usuario);

    fetch("https://localhost:7225/api/usuarios", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(usuario)
    })
    .then(response => response.json())
    .then(data => {
        console.log("Respuesta de la API:", data);
        alert("Usuario registrado exitosamente.");
        document.getElementById("registroForm").reset();
    })
    .catch(error => console.error("Error al registrar usuario:", error));
}
