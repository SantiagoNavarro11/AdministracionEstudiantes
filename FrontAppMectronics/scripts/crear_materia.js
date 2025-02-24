document.addEventListener("DOMContentLoaded", function () {
    cargarProfesores(); // Cargar profesores cuando la página cargue

    document.getElementById("formMateria").addEventListener("submit", function (event) {
        event.preventDefault(); // Evitar que se recargue la página
        registrarMateria();
    });
});

function cargarProfesores() {
    fetch("https://localhost:7225/api/usuarios")
        .then(response => response.json())
        .then(data => {
            const selectProfesores = document.getElementById("idUsuarioProfesor");
            selectProfesores.innerHTML = "<option disabled selected>Seleccione un profesor</option>";

            data.datos.forEach(profesor => {
                let option = document.createElement("option");
                option.value = profesor.idUsuario; // ID real del profesor
                option.textContent = `${profesor.nombres} ${profesor.apellidos}`; // Nombre en el select
                selectProfesores.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar profesores:", error));
}

function registrarMateria() {
    const nombreMateria = document.getElementById("nombreMateria").value;
    const numeroCreditos = document.getElementById("numeroCreditos").value;
    const idUsuarioProfesor = document.getElementById("idUsuarioProfesor").value;

    if (!nombreMateria || !numeroCreditos || !idUsuarioProfesor) {
        alert("Por favor, complete todos los campos.");
        return;
    }

    const nuevaMateria = {
        nombreMateria: nombreMateria,
        numeroCreditosMateria: parseInt(numeroCreditos),
        idUsuarioProfesor: parseInt(idUsuarioProfesor)
    };

    fetch("https://localhost:44335/api/Materia", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(nuevaMateria)
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(err => { throw err; });
        }
        return response.json();
    })
    .then(data => {
        console.log("Materia creada con éxito:", data);
        alert("Materia registrada correctamente!");
        document.getElementById("formMateria").reset();
    })
    .catch(error => {
        console.error("Error al registrar materia:", error);
        alert("Error al registrar la materia.");
    });
}
