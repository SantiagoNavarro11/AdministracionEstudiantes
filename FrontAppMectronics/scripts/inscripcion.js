document.addEventListener("DOMContentLoaded", function () {
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
            console.log("Usuarios recibidos:", data); // Verifica los datos recibidos

            const selectUsuarios = document.getElementById("IdUsuario");
            selectUsuarios.innerHTML = "<option disabled selected>Seleccione un usuario</option>";

            data.datos.forEach(usuario => {
                let option = document.createElement("option");
                option.value = usuario.idUsuario;  // Corregido
                option.textContent = usuario.nombres; // Corregido
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

            if (!data || !data.datos || !Array.isArray(data.datos)) {
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

            data.datos.forEach(materia => {
                let option = document.createElement("option");
                option.value = materia.idMateria;
                option.textContent = materia.nombre; 
                selectMaterias.appendChild(option);
            });
        })
        .catch(error => console.error("Error al cargar materias:", error));
}


function inscribirMateria() {

    debugger;

    const idUsuario = document.getElementById("IdUsuario").value;
    const idMateria = document.getElementById("IdMateria").value;

    const datos = {
        usuario: {
            idUsuario: parseInt(idUsuario)
        },
        materia: {
            idMateria: parseInt(idMateria)
        }        
    };

    console.log("Datos a enviar:", datos); // VERIFICAR DATOS ANTES DE ENVIAR

    fetch("https://localhost:44357/api/inscripcionMaterias", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(datos)
    })
    .then(response => response.json())
    .then(data =>
        {
            console.log("Respuesta del servidor:", data);
            alert(data.mensaje);
        })
    .catch(error => console.error("Error al inscribir:", error));
}
