    function toggleHide()
      {
        let htmlPreload =document.querySelector('#htmlPreload');
        
       htmlPreload.hidden = !htmlPreload.hidden;

         
      }
      let toggler = document.querySelector('#toggler');
      toggler.addEventListener('click', toggleHide);

      let hp  =document.querySelector('#htmlPreload');
      hp.src = "spinner.gif";
      hp.width = 200;
      
      


    
  var loadImage = new Image();

  loadImage.onload =
    () =>
    {
        console.log('onloaded');
        let pre = document.querySelector('#htmlPreload');
        pre.src = loadImage.src;
        pre.width = 800;

    };

loadImage.src = "LargeImage.jpg";
