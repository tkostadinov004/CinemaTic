var modal = document.getElementById('modal');
var modalOverlay = document.querySelector('.modal-overlay');
var openButtons = document.querySelectorAll('.open-popup-btn');
const openModal = () => {
    console.log(2);
    // Save current focus
  //  focusedElementBeforeModal = document.activeElement;
    // Listen for and trap the keyboard
    modal.addEventListener('keydown', trapTabKey);

    // Listen for indicators to close the modal
    if (modalOverlay) {
        modalOverlay.addEventListener('click', closeModal);
    }
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
    if (modalOverlay) {
        modalOverlay.classList.add('show');
    }
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

const filledStar = '<path d="M12 17.27l4.15 2.51c.76.46 1.69-.22 1.49-1.08l-1.1-4.72 3.67-3.18c.67-.58.31-1.68-.57-1.75l-4.83-.41-1.89-4.46c-.34-.81-1.5-.81-1.84 0L9.19 8.63l-4.83.41c-.88.07-1.24 1.17-.57 1.75l3.67 3.18-1.1 4.72c-.2.86.73 1.54 1.49 1.08l4.15-2.5z" style = "" > </path>';
const hollowStar = '<path fill="none" d="M0 0h24v24H0V0z"></path><path d="M19.65 9.04l-4.84-.42-1.89-4.45c-.34-.81-1.5-.81-1.84 0L9.19 8.63l-4.83.41c-.88.07-1.24 1.17-.57 1.75l3.67 3.18-1.1 4.72c-.2.86.73 1.54 1.49 1.08l4.15-2.5 4.15 2.51c.76.46 1.69-.22 1.49-1.08l-1.1-4.73 3.67-3.18c.67-.58.32-1.68-.56-1.75zM12 15.4l-3.76 2.27 1-4.28-3.32-2.88 4.38-.38L12 6.1l1.71 4.04 4.38.38-3.32 2.88 1 4.28L12 15.4z"></path>';
var currentStars = 0;
function fillStars(containers, starIndex) {
    hollowStars(containers, containers.length - 1);
    for (var k = 0; k <= starIndex; k++) {
        containers[k].lastElementChild.innerHTML = filledStar;
        containers[k].lastElementChild.classList.add('active');
    }
}
function hollowStars(containers, starIndex) {
    for (var k = 0; k <= starIndex; k++) {
        containers[k].lastElementChild.innerHTML = hollowStar;
        containers[k].lastElementChild.classList.remove('active');
    }
}
