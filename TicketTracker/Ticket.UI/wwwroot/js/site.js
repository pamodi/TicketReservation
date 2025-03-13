window.applyPhoneMask = () => {
    $('#phoneInput').inputmask('(999)-999-9999');
};

window.showToast = () => {
	let toastEl = document.getElementById('successToast');
	if (toastEl) {
		let toast = new bootstrap.Toast(toastEl);
		toast.show();
	}
};

window.showErrorToast = () => {
	let toastEl = document.getElementById('errorToast');
	if (toastEl) {
		let toast = new bootstrap.Toast(toastEl);
		toast.show();
	}
};

window.showPhoneErrorToast = () => {
	let toastEl = document.getElementById('phoneErrorToast');
	if (toastEl) {
		let toast = new bootstrap.Toast(toastEl);
		toast.show();
	}
};

window.restrictPhoneInput = () => {
	document.getElementById('phoneInput').addEventListener('input', function (e) {
		this.value = this.value.replace(/\D/g, ''); // Remove non-numeric characters
	});
};

function saveAsFile(filename, contentBase64) {
	const link = document.createElement('a');
	link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + contentBase64;
	link.download = filename;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
}

window.showLoginErrorToast = () => {
	let toastEl = document.getElementById('loginErrorToast');
	if (toastEl) {
		let toast = new bootstrap.Toast(toastEl);
		toast.show();
	}
};
