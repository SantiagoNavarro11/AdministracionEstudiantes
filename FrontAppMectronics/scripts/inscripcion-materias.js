document.addEventListener("DOMContentLoaded", function () {
    cargarMenu();
    cargarEstudiantes();
    cargarMaterias();
    cargarMateriasInscritas(IdUsuario());

    document.getElementById("inscripcionForm").addEventListener("submit", function (event) {
        event.preventDefault();
        inscribirMateria();
    });
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
                // Si el usuario logueado coincide, seleccionarlo automáticamente.
                if (usuario.idUsuario === usuarioLogueadoId) {
                    let option = document.createElement("option");
                    option.value = usuario.idUsuario;
                    option.textContent = `${usuario.nombres} ${usuario.apellidos}`;
                    option.selected = true;     

                    selectUsuarios.appendChild(option);
                }                
            });
        })
        .catch(error =>{
            alertaError(error);
        });
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
        .catch(error =>{
            alertaError(error);
        });
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
                cargarMateriasInscritas(IdUsuario());
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
                        <button class="btn btn-success btn-sm" onclick="cargarCompanerosMateria(${registro.materia.idMateria})" data-bs-toggle="modal" data-bs-target="#modalCompaneros">
                            Ver Compañeros
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


function cargarCompanerosMateria(idMateria,) {  
    fetch(URL_API_INSCRIBIR_MATERIAS + "inscripcionMaterias?IdMateria=" + idMateria)
    .then(response => response.json())
    .then(resultado => {
        let materiasArray = Array.isArray(resultado) ? resultado : resultado.datos;


        if (!materiasArray || !Array.isArray(materiasArray)) {
            return;
        }

        const tablaMaterias = document.getElementById("tablaEstudiantesMateria")
        tablaMaterias.innerHTML = "";
        
        materiasArray.forEach(registro => {
            let fila = document.createElement("tr");
            fila.innerHTML = `
                <td>${registro.usuario.nombres} ${registro.usuario.apellidos}</td>
            `;
            tablaMaterias.appendChild(fila);
        });
    })
    .catch(error => {
        alertaError(error);
    });
}
