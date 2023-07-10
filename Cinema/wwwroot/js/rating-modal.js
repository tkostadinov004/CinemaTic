const modal = document.getElementById('modal');
const modalOverlay = document.querySelector('.modal-overlay');
const openButtons = document.querySelectorAll('.open-popup-btn');
const openModal = () => {
    // Save current focus
  //  focusedElementBeforeModal = document.activeElement;

    // Listen for and trap the keyboard
    modal.addEventListener('keydown', trapTabKey);

    // Listen for indicators to close the modal
    modalOverlay.addEventListener('click', closeModal);
    // Close btn
    const closeBtn = document.querySelector('.close-btn');
    closeBtn.addEventListener('click', closeModal);

    //// submit form
    //const form = document.getElementById('review-form');
    //form.addEventListener('submit', submitopenButtons, false);

    // Find all focusable children
    var focusableElementsString = 'a[href], area[href], input:not([disabled]), select:not([disabled]), textarea:not([disabled]), button:not([disabled]), iframe, object, embed, [tabindex="0"], [contenteditable]';
    var focusableElements = modal.querySelectorAll(focusableElementsString);
    // Convert NodeList to Array
    focusableElements = Array.prototype.slice.call(focusableElements);

    var firstTabStop = focusableElements[0];
    var lastTabStop = focusableElements[focusableElements.length - 1];

    // Show the modal and overlay
    modal.classList.add('show');
    modalOverlay.classList.add('show');
    function trapTabKey(e) {
        // Check for TAB key press
        if (e.keyCode === 9) {

            // SHIFT + TAB
            if (e.shiftKey) {
                if (document.activeElement === firstTabStop) {
                    e.preventDefault();
                    lastTabStop.focus();
                }

                // TAB
            } else {
                if (document.activeElement === lastTabStop) {
                    e.preventDefault();
                    firstTabStop.focus();
                }
            }
        }

        // ESCAPE
        if (e.keyCode === 27) {
            closeModal();
        }
    }
};

const submitopenButtons = (e) => {
    // console.log(e);
    console.log('Form subbmitted!');
    e.preventDefault();
    closeModal();
};

const closeModal = () => {
    // Hide the modal and overlay
    modal.classList.remove('show');
    modalOverlay.classList.remove('show');

    //const form = document.getElementById('review-form');
    //form.reset();
    // Set focus back to element that had it before the modal was opened
   // focusedElementBeforeModal.focus();
};
openButtons.forEach(i => i.addEventListener('click', openModal));
function setFocus(evt) {
    const rateRadios = document.getElementsByName('rate');
    const rateRadiosArr = Array.from(rateRadios);
    const anyChecked = rateRadiosArr.some(radio => { return radio.checked === true; });
    // console.log('anyChecked', anyChecked);
    if (!anyChecked) {
        const star1 = document.getElementById('star1');
        star1.focus();
        // star1.checked = true;
    }
}

function editButtons(radio) {
    highlight(radio);

    var value = radio.value;
    var buttons = document.getElementsByClassName("star");
    var containers = document.getElementsByClassName("modal-star-container");
    for (var i = value; i < buttons.length; i++) {
        containers[i].children[1].style.color = "#dee81e";
    }
}
function highlight(radio) {
    console.log(radio);
    var value = radio.value;

    var buttons = document.getElementsByClassName("star");
    var containers = document.getElementsByClassName("modal-star-container");
    for (var i = 0; i < value; i++) {
        containers[i].children[1].style.color = "#909615";
    }
}