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

let membershipType = document.querySelector("#membership");

let APIUrlMembership = `https://localhost:7049/api/Member/GetMembershipTypes`;

fetch(APIUrlMembership)
  .then((response) => response.json())
  .then((data) => {
    // Kald funktion til at oprette listen med de modtagne data
    data.forEach((item) => {
      let option = document.createElement("option");
      option.value = item.membershipId;
      option.textContent = item.membershipType;
      option.setAttribute("data-description", item.description);

      membershipType.appendChild(option);
    });
  })
  .catch((error) => {
    console.error("Fejl ved hentning af data: ", error);
  });

let newmemberButton = document.querySelector("#newmemberButton");
let newMemberForm = document.querySelector(".newMemberForm");
let body = document.querySelector("body");
newmemberButton.addEventListener("click", () => {
  newMemberForm.classList.toggle("show");
  body.classList.toggle("no-scroll");
});

let submitButton = document.querySelector("#submitButton");

submitButton.addEventListener("click", () => {
  let name = document.querySelector("#name").value;
  let phone = document.querySelector("#phone").value;
  let email = document.querySelector("#email").value;
  let membership = document.querySelector("#membership").selectedOption.value;
  let membershipType =
    document.querySelector("#membership").selectedOption.textContent;
  let description = document
    .querySelector("#membership")
    .selectedOption.getAttribute("data-description");

  let newMember = {
    name: name,
    phoneNumber: phone,
    email: email,
    membership: {
      membershipId: membership,
      membershipType: membershipType,
      description: description,
    },
  };

  console.log(newMember);

  let APIUrl = `https://localhost:7049/api/Member`;

  fetch(APIUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newMember),
  })
    .then((response) => response.json())

    .catch((error) => {
      console.error("Fejl ved oprettelse af data: ", error);
    });
});
