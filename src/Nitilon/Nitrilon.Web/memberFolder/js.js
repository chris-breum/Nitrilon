let APIUrl = `https://localhost:7049/api/Member/all`;

fetch(APIUrl)
  .then((response) => response.json())
  .then((data) => {
    // Kald funktion til at oprette listen med de modtagne data
    createList(data);
  })
  .catch((error) => {
    console.error("Fejl ved hentning af data: ", error);
  });
function createList(data) {
  const formContainer = document.querySelector("section");

  const table = document.createElement("table");

  data.forEach((item) => {
    const row = document.createElement("tr");

    const nameCell = document.createElement("td");
    nameCell.textContent = `Name: ${item.name}`;
    row.appendChild(nameCell);

    const phoneNumberCell = document.createElement("td");
    phoneNumberCell.textContent = `Phone Number: ${item.phoneNumber}`;
    row.appendChild(phoneNumberCell);

    const emailCell = document.createElement("td");
    emailCell.textContent = `Email: ${item.email}`;
    row.appendChild(emailCell);

    const membershipTypeCell = document.createElement("td");
    membershipTypeCell.textContent = `Membership Type: ${item.membership.membershipType}`;
    row.appendChild(membershipTypeCell);

    const deleteCell = document.createElement("td");
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Fjern medlem";
    deleteButton.addEventListener("click", () => {
      // Call a function to handle the delete action
      deleteItem(item.memberId);
    });
    deleteCell.appendChild(deleteButton);
    row.appendChild(deleteCell);

    table.appendChild(row);
  });

  formContainer.appendChild(table);
}

function deleteItem(id) {
  let APIUrl = `https://localhost:7049/api/Member/${id}`;

  fetch(APIUrl, {
    method: "DELETE",
  })
    .then((response) => response.json())
    .then((data) => {
      // Genindlæs siden
    })
    .catch((error) => {
      console.error("Fejl ved sletning af data: ", error);
    });
  location.reload();
}
