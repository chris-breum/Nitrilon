const CurrentDate = new Date();
let APIUrl = "https://localhost:7049/api/Event/date";

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
  const listContainer = document.getElementById("list-container");
  const ul = document.createElement("ul");

  data.forEach((item, index) => {
    const li = document.createElement("li");

    // Opret en unik id for hvert element
    const itemId = `item-${index}`;

    // Tilføj id attribut til li elementet
    li.setAttribute("id", itemId);

    // Opret teksten, der skal vises (dato og navn)
    const text = document.createTextNode(`${item.date} - ${item.name}`);

    // Tilføj teksten til li elementet
    li.appendChild(text);

    // Tilføj li elementet til ul elementet
    ul.appendChild(li);
  });

  // Tilføj ul elementet til listContainer
  listContainer.appendChild(ul);
}
