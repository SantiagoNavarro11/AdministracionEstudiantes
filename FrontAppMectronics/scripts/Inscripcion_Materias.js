document.addEventListener("DOMContentLoaded", function () {
    cargarMenu();
    cargarUsuarios();
    cargarMaterias();

    document.getElementById("inscripcionForm").addEventListener("submit", function (event) {
        event.preventDefault();
        inscribirMateria();
    });
});

function cargarUsuarios() {
    fetch("https://localhost:7225/api/usuarios")
        .then(response => response.json())
        .then(data => {
            console.log("Usuarios recibidos:", data);

            let usuariosArray = Array.isArray(data) ? data : data.datos;
            if (!usuariosArray || !Array.isArray(usuariosArray)) {
                console.error("No se recibió un array de usuarios.");
                return;
            }

            const selectUsuarios = document.getElementById("IdUsuario");
            selectUsuarios.innerHTML = "";

            let defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Seleccione un usuario";
            defaultOption.disabled = true;
            selectUsuarios.appendChild(defaultOption);

            // Recuperar usuario logueado desde localStorage
            let usuarioActual = JSON.parse(localStorage.getItem("usuarioactual"));
            let usuarioLogueadoId = usuarioActual ? usuarioActual.idUsuario : null;

            usuariosArray.forEach(usuario => {
                let option = document.createElement("option");
                option.value = usuario.idUsuario;
                option.textContent = `${usuario.nombres} ${usuario.apellidos}`;
                
                // Si el usuario logueado coincide, seleccionarlo automáticamente
                if (usuario.idUsuario === usuarioLogueadoId) {
                    option.selected = true;
                }

                selectUsuarios.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar usuarios:", error));
}


function cargarMaterias() {
    fetch("https://localhost:44335/api/Materia")
        .then(response => response.json())
        .then(data => {
            console.log("Materias recibidas:", data);

            let materiasArray = Array.isArray(data) ? data : data.datos;
            if (!materiasArray || !Array.isArray(materiasArray)) {
                console.error("No se recibió un array de materias.");
                return;
            }

            const selectMaterias = document.getElementById("IdMateria");
            selectMaterias.innerHTML = "";

            let defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Seleccione una materia";
            defaultOption.disabled = true;
            defaultOption.selected = true;
            selectMaterias.appendChild(defaultOption);

            materiasArray.forEach(materia => {
                let option = document.createElement("option");
                option.value = materia.idMateria;
                option.textContent = materia.nombre;
                selectMaterias.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar materias:", error));
}

function inscribirMateria() {

    const idUsuario = document.getElementById("IdUsuario").value;
    const idMateria = document.getElementById("IdMateria").value;

    // Verifica que ambos valores no estén vacíos
    if (!idUsuario || !idMateria) {
        alert("Por favor, selecciona una materia.");
        return;
    }

    const inscripcion = {
        usuario: {
            idUsuario: parseInt(idUsuario)
        },
        materia: {
            idMateria: parseInt(idMateria)
        }  
    };

    console.log("Datos a enviar:", inscripcion);

    fetch("https://localhost:44357/api/inscripcionMaterias", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(inscripcion)
    })
    .then(response => response.json())
        .then(resultado => {
            if (resultado.exito) {

                cargarMaterias()
                alertaConfirmacion(resultado.mensaje);
                document.getElementById("inscripcionForm").reset();
            }
            else {
                alertaError(resultado.mensaje);
            }
        })
        .catch(error => {
            alertaError(error);
        });
    }
