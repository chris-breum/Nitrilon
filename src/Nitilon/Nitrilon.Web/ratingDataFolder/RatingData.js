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
  let goodRatingCount = 0;
  let neutralRatingCount = 0;
  let badRatingCount = 0;
  fetch(`https://localhost:7049/api/EventRating?eventId=${eventId}`)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Anmodningen mislykkedes.");
      }
      return response.json();
    })

    .then((data) => {
      // Nu har du data tilgængelig i JavaScript
      var badRatingCount = data.badRatingCount;
      var neutralRatingCount = data.neutralRatingCount;
      var goodRatingCount = data.goodRatingCount;

      // Gør noget med disse værdier her
      createChart(goodRatingCount, neutralRatingCount, badRatingCount);
    })

    .catch((error) => {
      console.error("Fejl ved hentning af ratingdata:", error);
    });
}

// Function to create a chart
/**
 * Creates a chart using Chart.js based on the provided happy, neutral, and sad values.
 *
 * @param {number} happy - The number of happy values.
 * @param {number} neutral - The number of neutral values.
 * @param {number} sad - The number of sad values.
 */
/**
 * Creates a chart using Chart.js based on the provided happy, neutral, and sad values.
 *
 * @param {number} happy - The number of happy values.
 * @param {number} neutral - The number of neutral values.
 * @param {number} sad - The number of sad values.
 */
function createChart(happy, neutral, sad) {
  // Create a chart using Chart.js

  if (myChart && typeof myChart.destroy === "function") {
    myChart.destroy();
  }
  var ctx = document.querySelector("#myChart").getContext("2d");

  myChart = new Chart(ctx, {
    type: "bar",
    data: {
      labels: ["Glad - " + happy, "Neutral - " + neutral, "Sur - " + sad],
      datasets: [
        {
          label: "procent",
          data: [happy, neutral, sad].map(
            (value) => (value / (happy + neutral + sad)) * 100
          ),
          backgroundColor: [
            "rgba(40, 204, 55, 0.6)", // Green for glad
            "rgba(242, 203, 4, 0.6)", // Yellow for neutral
            "rgba(235, 8, 27, 0.6)", // Red for sur
          ],
          borderColor: [
            "rgba(40, 204, 55, 1)",
            "rgba(242, 203, 4, 1)",
            "rgba(235, 8, 27, 1)",
          ],
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            callback: function (value) {
              return value + "%";
            },
            color: "white",
            font: {
              size: 14, // Skriftstørrelse for y-aksen
            },
          },
        },
        x: {
          ticks: {
            color: "white",
            font: {
              size: 14, // Skriftstørrelse for x-aksen
            },
          },
        },
      },
      layout: {
        maintainAspectRatio: false,
      },

      plugins: {
        legend: {
          display: false,
        },
        tooltip: {
          callbacks: {
            label: function (context) {
              var totalCount = happy + neutral + sad;
              var label = context.dataset.label || "";
              if (label) {
                label += ": ";
              }
              if (context.parsed.y !== null) {
                label = context.parsed.y.toFixed(2) + "%";
              }
              return label;
            },
          },
        },
      },
    },
  });

  // Display the count of each value

  var totalCount = happy + neutral + sad;
  var countSection = document.createElement("div");
  countSection.style.color = "white"; // Set the text color to white
  countSection.textContent =
    "Antal: Glad - " +
    happy +
    ", Neutral - " +
    neutral +
    ", Sur - " +
    sad +
    ", Total - " +
    totalCount;
  document.body.appendChild(countSection);
}
