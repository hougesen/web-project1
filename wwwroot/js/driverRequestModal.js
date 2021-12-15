/**
 * @param {number} routeId
 * @returns {Promise<{ userId: number, routeId: number, name: string, phoneNumber: string, email: string }[]>}
 */
async function fetchRequestToDrive(routeId) {
    return await fetch(`/api/routes/request/${routeId}`)
        .then((res) => res.json(res) || [])
        .catch((error) => {
            console.error('Error fetching requests to drive', error);
            return [];
        });
}

async function generateRequestModal(routeId) {
    const driveRequests = await fetchRequestToDrive(routeId);

    const modalBody = document.querySelector('#req-modal-body');
    if (driveRequests?.length) {
        for (const request of driveRequests) {
            const tr = document.createElement('tr');
            const nameTd = document.createElement('td');
            nameTd.appendChild(document.createTextNode(request.name));
            tr.appendChild(nameTd);

            const phoneTd = document.createElement('td');
            phoneTd.appendChild(document.createTextNode(request.phoneNumber));
            tr.appendChild(phoneTd);

            const emailTd = document.createElement('td');
            emailTd.appendChild(document.createTextNode(request.email));
            tr.appendChild(emailTd);

            const button = document.createElement('button');
            button.appendChild(document.createTextNode('Tildel'));
            button.classList.add('button');
            button.addEventListener('click', () => assignUserToRoute(request.routeId, request.userId));

            tr.appendChild(button);

            modalBody.appendChild(tr);
        }
    } else {
        const para = document.createElement('p');
        para.appendChild(document.createTextNode('Ingen anmodninger om at køre turen'));

        modalBody.appendChild(para);
    }

    document.querySelector('#req-modal').style.display = 'block';
}

async function assignUserToRoute(routeId, userId) {
    await fetch(`/api/routes/${routeId}/${userId}`, { method: 'PUT' })
        .then(() => alert('Chaufføren fik tildelt ruten'))
        .catch((error) => {
            console.error('assignUserToRoute error', error);
            alert('Hov, noget gik galt. Prøv venligt igen');
        });

    window.location.reload();
}

function closeRequestsModal() {
    document.querySelector('#req-modal').style.display = null;
    const modalBody = document.querySelector('#req-modal-body');

    while (modalBody.firstChild) {
        modalBody.removeChild(modalBody.firstChild);
    }
}

window.onclick = (event) => {
    if (event.target === document.querySelector('#req-modal')) {
        closeRequestsModal();
    }
};
