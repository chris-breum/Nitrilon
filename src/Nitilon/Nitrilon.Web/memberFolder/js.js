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
  const form = document.createElement("form");

  const fragment = document.createDocumentFragment();

  data.forEach((item) => {
    const div = document.createElement("div");

    const p1 = document.createElement("p");
    p1.textContent = `Navn: ${item.name}`;
    div.appendChild(p1);

    const p2 = document.createElement("p");
    p2.textContent = `tlf: ${item.phoneNumber}`;
    div.appendChild(p2);

    const p3 = document.createElement("p");
    p3.textContent = `Email: ${item.email}`;
    div.appendChild(p3);

    const p4 = document.createElement("p");
    p4.textContent = `Medlems Type: ${item.membership.membershipType}`;
    div.appendChild(p4);

    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Fjern medlem";
    deleteButton.addEventListener("click", () => {
      // Call a function to handle the delete action
      deleteItem(item.id);
    });
    div.appendChild(deleteButton);

    fragment.appendChild(div);
  });

  form.appendChild(fragment);

  // Tilføj form elementet til formContainer
  formContainer.appendChild(form);
}

function deleteItem(id) {
  let APIUrl = `https://localhost:7049/api/Member/${id}`;

  fetch(APIUrl, {
    method: "DELETE",
  })
    .then((response) => response.json())
    .then((data) => {
      // Genindlæs siden
      location.reload();
    })
    .catch((error) => {
      console.error("Fejl ved sletning af data: ", error);
    });
}
