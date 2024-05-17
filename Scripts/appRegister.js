const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');
const boton = document.getElementById('btn');
const expresiones = {
	Name: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
	password: /^.{4,12}$/, // 4 a 12 digitos.
	Email: /^[a-zA-Z0-9_.+-]+@[gmail]+.[com]+$/,
	phone: /^\d{7,10}$/ // 7 a 14 numeros.
}

const campos = {
	Name: false,
	password: false,
	Email: false,
	phone: false
}

const validarFormulario = (e) => {
	switch (e.target.name) {
		case "Name":
			validarCampo(expresiones.Name, e.target, 'Name');
			break;
		case "password":
			validarCampo(expresiones.password, e.target, 'password');
			break;
		case "Email":
			validarCampo(expresiones.Email, e.target, 'Email');
			break;
		case "phone":
			validarCampo(expresiones.phone, e.target, 'phone');
			break;
	}
}

const validarCampo = (expresion, input, campo) => {
	if (expresion.test(input.value)) {
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-check-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-times-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.remove('formulario__input-error-activo');
		campos[campo] = true;
	} else {
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-times-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-check-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.add('formulario__input-error-activo');
		campos[campo] = false;
	}
}

inputs.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});

btn.addEventListener('click', (e) => {

	const terminos = document.getElementById('terminos');
	if (campos.Name && campos.password && campos.Email && campos.phone && terminos.checked) {


		document.getElementById('formulario__mensaje-exito').classList.add('formulario__mensaje-exito-activo');
		setTimeout(() => {
			document.getElementById('formulario__mensaje-exito').classList.remove('formulario__mensaje-exito-activo');
		}, 5000);

		document.querySelectorAll('.formulario__grupo-correcto').forEach((icono) => {
			icono.classList.remove('formulario__grupo-correcto');
		});
	} else {

		e.preventDefault()
		formulario.reset();
		document.getElementById('formulario__mensaje').classList.add('formulario__mensaje-activo');
	}
});