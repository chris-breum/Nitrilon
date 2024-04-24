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
  let happy = 0;
  let neutral = 0;
  let sad = 0;
  fetch(`https://localhost:7049/api/EventRating/${eventId}`)
    .then((response) => response.json())
    .then((ratings) => {
      // Process the ratings
      ratings.forEach((rating) => {
        // Do something with each rating
        console.log(rating);
        if (rating.ratingId === 1) {
          happy++;
        } else if (rating.ratingId === 2) {
          neutral++;
        } else if (rating.ratingId === 3) {
          sad++;
        }
      });

      // Create chart after processing ratings
      createChart(happy, neutral, sad);
      console.log(happy, neutral, sad);
    })
    .catch((error) => {
      console.error("Error fetching ratings:", error);
    });
}

// Function to create a chart
function createChart(happy, neutral, sad) {
  // Create a chart using Chart.js

  if (myChart && typeof myChart.destroy === "function") {
    myChart.destroy();
  }
  var ctx = document.querySelector("#myChart").getContext("2d");

  myChart = new Chart(ctx, {
    type: "bar",
    data: {
      labels: ["glad ", "neutral ", "sur "],
      datasets: [
        {
          label: "Værdi",
          data: [happy, neutral, sad],
          backgroundColor: [
            "rgba(255, 99, 132, 0.2)",
            "rgba(54, 162, 235, 0.2)",
            "rgba(75, 192, 192, 0.2)",
          ],
          borderColor: [
            "rgba(255, 99, 132, 1)",
            "rgba(54, 162, 235, 1)",
            "rgba(75, 192, 192, 1)",
          ],
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      plugins: {
        legend: {
          display: false,
        },
        tooltip: {
          callbacks: {
            label: function (context) {
              var label = context.dataset.label || "";
              if (label) {
                label += ": ";
              }
              if (context.parsed.y !== null) {
                label += context.parsed.y;
              }
              return label;
            },
          },
        },
      },
    },
  });
}
