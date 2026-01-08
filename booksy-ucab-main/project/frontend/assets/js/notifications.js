
export function showNotification(message, color, type = 'info') {

    const notificationsArea = document.getElementById('notifications-area');
    if (!notificationsArea){
        console.error('No hay un area de notificaciones definida');
    }

    const notification = document.createElement('div');
    notification.textContent = message;
    notification.style.backgroundColor = color;
    notification.classList.add('notification-item', type);

    // Crear botón de cerrar
    const closeButton = document.createElement('button');
    closeButton.textContent = 'X';
    closeButton.classList.add('notification-close-btn');
    notification.appendChild(closeButton);

    notificationsArea.appendChild(notification);

    // Función para cerrar la notificación
    const dismissNotification = (notificationElement) => {
        if (!notificationElement.classList.contains('fade-out')) {
            notificationElement.classList.add('fade-out');
            // Eliminar el elemento después de que la transición termine (0.5s)
            setTimeout(() => {
                notificationElement.remove();
            }, 500); 
        }
    };

    // Evento para el botón de cerrar
    closeButton.addEventListener('click', () => {
        dismissNotification(notification);
    });

    // Desaparición automática después de 10 segundos
    setTimeout(() => {
        dismissNotification(notification);
    }, 10000); // 10 segundos
}