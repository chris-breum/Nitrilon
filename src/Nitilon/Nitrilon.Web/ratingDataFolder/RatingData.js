// Fetch events from API
fetch("https://localhost:7049/api/Event/all")
  .then((response) => response.json())
  .then((data) => {
    // Get the <section> element
    const section = document.querySelector("section");

    // Create <ul> element
    const ul = document.createElement("ul");

    // Loop through the events and create <li> elements
    data.forEach((event) => {
      const li = document.createElement("li");
      li.textContent = event.name;
      li.addEventListener("click", () => {
        findRatingsByEventId(event.id);
      });
      ul.appendChild(li);
    });

    // Append <ul> to <section>
    section.appendChild(ul);
  })
  .catch((error) => {
    console.error("Error fetching events:", error);
  });

// Function to find ratings by event ID
function findRatingsByEventId(eventId) {
  // Fetch ratings from API
  fetch(`https://localhost:7049/api/Rating/event/${eventId}`)
    .then((response) => response.json())
    .then((ratings) => {
      // Process the ratings
      ratings.forEach((rating) => {
        // Do something with each rating
        console.log(rating);
      });
    })
    .catch((error) => {
      console.error("Error fetching ratings:", error);
    });
}

// Call the function with the event ID
const eventId = 123; // Replace with the actual event ID
