document.addEventListener("DOMContentLoaded", function () {
    cargarMenu();
    cargarEstudiantes();
    cargarMaterias();

    if (IdRol() == ROL_ADMINISTRADOR) {
        cargarMateriasInscritas('');
    }

    document.getElementById("inscripcionForm").addEventListener("submit", function (event) {
        event.preventDefault();
        inscribirMateria();
    });

    /*document.getElementById("miSelect").addEventListener("change", function () {
        let valorSeleccionado = this.value;
        document.getElementById("resultado").textContent = valorSeleccionado;
    });*/
});

function cargarEstudiantes() {
    fetch(URL_API_USUARIOS + "usuarios?IdRol=" + ROL_ESTUDIANTE)
        .then(response => response.json())
        .then(data => {
            let usuariosArray = Array.isArray(data) ? data : data.datos;

            if (!usuariosArray || !Array.isArray(usuariosArray)) {
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
            let usuarioLogueadoId = IdUsuario();

            usuariosArray.forEach(usuario => {
                let option = document.createElement("option");
                option.value = usuario.idUsuario;
                option.textContent = `${usuario.nombres} ${usuario.apellidos}`;

                // Si el usuario logueado coincide, seleccionarlo automáticamente, solo si el rol no es administrador.
                if (usuario.idUsuario === usuarioLogueadoId && IdRol() != ROL_ADMINISTRADOR) {
                    option.selected = true;
                    cargarMateriasInscritas(usuario.idUsuario);
                }

                selectUsuarios.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar usuarios:", error));
}


function cargarMaterias() {
    fetch(URL_API_MATERIAS + "materia")
        .then(response => response.json())
        .then(data => {

            let materiasArray = Array.isArray(data) ? data : data.datos;
            if (!materiasArray || !Array.isArray(materiasArray)) {
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
        alertaAdvertencia("Por favor, seleccione un usuario y una materia.");
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

    fetch(URL_API_INSCRIBIR_MATERIAS + "inscripcionMaterias", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(inscripcion)
    })
        .then(response => response.json())
        .then(resultado => {         
            if (resultado.exito) {
                cargarMateriasInscritas('');
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

function cargarMateriasInscritas(idUsuario) {
    var url = URL_API_INSCRIBIR_MATERIAS + `inscripcionMaterias/?IdUsuario=${idUsuario}`;

    if (idUsuario == '')
        url = URL_API_INSCRIBIR_MATERIAS + `inscripcionMaterias`;

    fetch(url)
        .then(response => response.json())
        .then(resultado => {
            let materiasArray = Array.isArray(resultado) ? resultado : resultado.datos;

            if (!materiasArray || !Array.isArray(materiasArray)) {
                return;
            }

            const tablaMaterias = document.getElementById("tablaMateriasHabilitadas")
            tablaMaterias.innerHTML = "";

            materiasArray.forEach(registro => {
                let fila = document.createElement("tr");

                fila.innerHTML = `
                    <td>${registro.usuario.nombres} ${registro.usuario.apellidos}</td>
                    <td>${registro.materia.nombre}</td>
                    <td>${registro.materia.numeroCreditos}</td>
                    <td>${registro.materia.nombreProfesor}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="cargarMateriaParaEditar(${registro.materia.idMateria})" data-bs-toggle="modal" data-bs-target="#modalRegistroMateria">
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
