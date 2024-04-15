var RedSmiley = document.querySelector("#redSmiley");
var YellowSmiley = document.querySelector("#yellowSmiley");
var GreenSmiley = document.querySelector("#greenSmiley");
let Event = 1;
const APIUrl = 'https://localhost:7049/api/EventRating';
let rating = 0;
const test = {
         method: 'POST',
         headers: {
            'Content-Type': 'application/json'
         },
         body: JSON.stringify({ EventId: Event,  RatingId: rating })
      }

RedSmiley.addEventListener("click", sendToServer());

YellowSmiley.addEventListener("click", sendToServer());

GreenSmiley.addEventListener("click", sendToServer());




   function sendToServer() {
      // Send the rating to the server
      fetch(APIUrl, test)
      
      .then(response => {
         if (response.ok) {
            console.log('Rating sent successfully');
         } else {
            console.error('Failed to send rating');
         }
      })
      .catch(error => {
         console.error('Error sending rating:', error);
      });
   }



