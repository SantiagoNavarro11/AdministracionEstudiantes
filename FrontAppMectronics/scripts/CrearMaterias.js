document.addEventListener("DOMContentLoaded", function () {
    cargarProfesores();
    cargarMaterias()
    cargarMenu();
    cargarRoles();

    document.getElementById("formMateria").addEventListener("submit", function (event) {
        event.preventDefault();
        registrarMateria();
    });
});

function cargarMaterias() {
    fetch(URL_API_MATERIAS + "Materia")
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error HTTP: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log("Materias recibidas:", data);

            let materiasArray = Array.isArray(data) ? data : data.datos;

            if (!materiasArray || !Array.isArray(materiasArray)) {
                console.error("No se recibió un array de materias.");
                return;
            }

            const tablaMaterias = document.getElementById("tablaMaterias")
            tablaMaterias.innerHTML = "";

            materiasArray.forEach(materia => {
                let fila = document.createElement("tr");

                fila.innerHTML = `
                    <td>${materia.idMateria}</td>
                    <td>${materia.nombre}</td>
                    <td>${materia.numeroCreditos}</td>
                    <td>${materia.idUsuarioProfesor}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="cargarMateriaParaEditar(${materia.idMateria})" data-bs-toggle="modal" data-bs-target="#modalRegistroMateria">
                            Editar
                        </button>
                    </td>
                `;

                tablaMaterias.appendChild(fila);
            });
        })
        .catch(error => {
            console.error("Error al cargar las materias:", error);
            alertaError("No se pudieron cargar las materias.");
        });
}


function cargarProfesores() {
    fetch("https://localhost:7225/api/usuarios?IdRol=" + ROL_PROFESOR)
        .then(response => response.json())
        .then(data => {
            const selectProfesores = document.getElementById("idUsuarioProfesor");
            selectProfesores.innerHTML = "<option disabled selected>Seleccione un profesor</option>";
debugger;
            data.datos.forEach(profesor => {
                let option = document.createElement("option");
                option.value = profesor.idUsuario;
                option.textContent = `${profesor.nombres} ${profesor.apellidos}`;

                selectProfesores.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar profesores:", error));
}


function registrarMateria() {

    const nombre = document.getElementById("nombre").value;
    const numeroCreditos = document.getElementById("numeroCreditos").value;
    const idUsuarioProfesor = document.getElementById("idUsuarioProfesor").value;

    const nuevaMateria = {
        nombre: nombre,
        numeroCreditos: parseInt(numeroCreditos),
        idUsuarioProfesor: parseInt(idUsuarioProfesor)
    };

    fetch("https://localhost:44335/api/Materia", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(nuevaMateria)
    })
        .then(response => response.json())
        .then(resultado => {
            if (resultado.exito) {

                cargarMaterias()
                alertaConfirmacion(resultado.mensaje);
                document.getElementById("formMateria").reset();
            }
            else {
                alertaError(resultado.mensaje);
            }
        })
        .catch(error => {
            alertaError(error);
        });
}



function cargarMateriaParaEditar(idmateria) {

    fetch(URL_API_MATERIAS + `Materia/${idmateria}`)
        .then(response => response.json())
        .then(resultado => {
            document.getElementById("nombre").value = resultado.datos.nombre;
            document.getElementById("numeroCreditos").value = resultado.datos.numeroCreditos;
            document.getElementById("idUsuarioProfesor").value = resultado.datos.idUsuarioProfesor;
        })
        .catch(error => {
            alertaError(error);
        });
}

//Actualiza una materia.
function actualizarUsuario() {
    let idmateria = document.getElementById("idmateria").value;

    let materia = {
        idmateria: parseInt(idmateria),
        nombre: document.getElementById("nombres").value,
        numeroCreditos: document.getElementById("numeroCreditos").value,
        roles: {
            idRol: parseInt(document.getElementById("IdRoles").value),
        }
    };

    fetch(URL_API_MATERIAS + `Materia`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(usuario)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al actualizar el usuario.");
            }
            return response.json();
        })
        .then(data => {
            // Restablecer el formulario
            document.getElementById("registroForm").reset();
            document.getElementById("idmateria").value = "";

            cerrarModal();
            alertaAdvertencia("Usuario actualizado exitosamente.");
            cargarMaterias();
        })
        .catch(error => {
            cerrarModal();
            alertaError(error);
        });
}