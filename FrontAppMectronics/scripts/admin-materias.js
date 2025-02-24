document.addEventListener("DOMContentLoaded", function () {
    cargarMenu();
    cargarMaterias()
    cargarProfesores();

    document.getElementById("formMateria").addEventListener("submit", function (event) {
        event.preventDefault();
        guardarMateria();
    });
});

function cargarMaterias() {
    fetch(URL_API_MATERIAS + "materia")
        .then(response => response.json())
        .then(resultado => {
            let materiasArray = Array.isArray(resultado) ? resultado : resultado.datos;

            if (!materiasArray || !Array.isArray(materiasArray)) {
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
                    <td>${materia.nombreProfesor}</td>
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
            alertaError(error);
        });
}


function cargarProfesores() {
    fetch(URL_API_USUARIOS + "usuarios?IdRol=" + ROL_PROFESOR)
        .then(response => response.json())
        .then(resultado => {
            const selectProfesores = document.getElementById("idUsuarioProfesor");
            selectProfesores.innerHTML = "<option disabled selected>Seleccione un profesor</option>";

            resultado.datos.forEach(profesor => {
                let option = document.createElement("option");
                option.value = profesor.idUsuario;
                option.textContent = `${profesor.nombres} ${profesor.apellidos}`;

                selectProfesores.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar profesores:", error));
}

async function guardarMateria() {
    let idmateria = document.getElementById("idMateria").value;

    if (!idmateria) {
        registrarMateria();
    }
    else {
        actualizarMateria();
    }
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

    fetch(URL_API_MATERIAS + "materia", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(nuevaMateria)
    })
        .then(response => response.json())
        .then(resultado => {
            cerrarModal();
            if (resultado.exito) {
                alertaConfirmacion(resultado.mensaje);
                document.getElementById("formMateria").reset();
                document.getElementById("idMateria").value = "";
                cargarMaterias();
            }
            else {
                cerrarModal();
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
            document.getElementById("idMateria").value = resultado.datos.idMateria;
            document.getElementById("nombre").value = resultado.datos.nombre;
            document.getElementById("numeroCreditos").value = resultado.datos.numeroCreditos;
            document.getElementById("idUsuarioProfesor").value = resultado.datos.idUsuarioProfesor;
        })
        .catch(error => {
            alertaError(error);
        });
}

//Actualiza una materia.
function actualizarMateria() {
    let idmateria = document.getElementById("idMateria").value;
    const nombre = document.getElementById("nombre").value;
    const numeroCreditos = document.getElementById("numeroCreditos").value;
    const idUsuarioProfesor = document.getElementById("idUsuarioProfesor").value;

    const materia = {
        idMateria: parseInt(idmateria),
        nombre: nombre,
        numeroCreditos: parseInt(numeroCreditos),
        idUsuarioProfesor: parseInt(idUsuarioProfesor)
    };

    fetch(URL_API_MATERIAS + 'materia', {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(materia)
    })
        .then(response => response.json())
        .then(resultado => {
            cerrarModal();
            if (resultado.exito) {
                alertaAdvertencia(resultado.mensaje);
                document.getElementById("formMateria").reset();
                document.getElementById("idMateria").value = "";
                cargarMaterias();
            }
            else {
                alertaAdvertencia(resultado.mensaje);
            }
        })
        .catch(error => {
            cerrarModal();
            alertaError(error);
        });
}

function cerrarModal() {

    var modalElement = document.getElementById('modalRegistroMateria');
    var modalInstance = bootstrap.Modal.getInstance(modalElement); // Obtener la instancia del modal

    if (modalInstance) {
        modalInstance.hide(); // Cierra la modal
    }
}