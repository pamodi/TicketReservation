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