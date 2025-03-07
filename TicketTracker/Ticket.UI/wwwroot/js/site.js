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

window.restrictPhoneInput = () => {
	document.getElementById('phoneInput').addEventListener('input', function (e) {
		this.value = this.value.replace(/\D/g, ''); // Remove non-numeric characters
	});
};
