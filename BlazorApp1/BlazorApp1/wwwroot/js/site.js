window.bootstrapModalClose = (modalId) => {
    const modalEl = document.querySelector(modalId);
    if (!modalEl) return;

    const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
    modal.hide();

    // Delay cleanup slightly so Blazor doesn’t interfere with Bootstrap’s animations
    setTimeout(() => {
        document.querySelectorAll('.modal-backdrop').forEach(b => b.remove());
        document.body.classList.remove('modal-open');
        document.body.style.removeProperty('padding-right');
    }, 300); // 300ms = default Bootstrap fade-out duration
};
