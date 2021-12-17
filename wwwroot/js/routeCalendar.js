/**
 * @returns {Promise<number[] | null>}
 */
async function fetchRoutesByDay() {
  return await fetch('/api/routes/calendar')
    .then((res) => res.json())
    .catch((error) => {
      console.error('Error fetching calendar', error);
      return null;
    });
}

/**
 * @summary looks back to the monday closest to the 1. of this month.
 * @param {Date} date
 * @returns {Date}
 */
function getMondayClosest(date = null) {
  if (!date) {
    date = new Date(new Date(new Date().setDate(1)).setHours(0, 0, 0, 0));
  }

  if (date.getDay() === 1) {
    return date;
  }

  return getMondayClosest(new Date(new Date(date).setDate(date.getDate() - 1)));
}

async function generateCalendar() {
  const routeCountArr = await fetchRoutesByDay();

  const startDate = getMondayClosest();
  // Sets the date to last day of month
  let endDate = new Date(
    new Date(new Date(new Date().setMonth(new Date().getMonth() + 1)).setDate(0)).setHours(23, 59, 59, 59),
  );
  // Days between startDate and endDate
  const timeBetween = (endDate.getTime() - startDate.getTime()) / (1000 * 3600 * 24);
  // Adds any extra days needed to make the total days mod 7 give 0
  endDate = new Date(endDate.setDate(endDate.getDate() + Math.round(7 - (timeBetween % 7))));

  const calendar = document.querySelector('#calendar');
  const dateClasses = ['p-4', 'bg-slate-200', 'rounded-xl', 'flex', 'flex-col', 'cursor-pointer'];

  let i = 0;
  let iDate = new Date(startDate);
  while (iDate <= endDate) {
    const dateWrapper = document.createElement('div');
    dateWrapper.classList.add(...dateClasses);

    const datePara = document.createElement('p');
    const currentDate = document.createTextNode(iDate.getDate());

    const placeholderDate = new Date(iDate);

    // Redirect to routes on click
    dateWrapper.addEventListener('click', () => {
      window.location.href = `${window.location.origin}/routes?searchString=${placeholderDate.getFullYear()}-${
        placeholderDate.getMonth() + 1
      }-${placeholderDate.getDate()}`;
    });

    datePara.appendChild(currentDate);
    dateWrapper.appendChild(datePara);

    if (iDate.getMonth() === new Date().getMonth()) {
      // Insert amount of routes on the given day
      const routeCountHeading = document.createElement('h3');
      routeCountHeading.classList.add(...['font-bold', 'text-lg']);

      const routeCountText = document.createTextNode(
        `${routeCountArr[i]} ${routeCountArr[i] === 1 ? 'rute' : 'ruter'}`,
      );
      routeCountHeading.appendChild(routeCountText);
      dateWrapper.appendChild(routeCountHeading);
      i += 1;
    } else {
      dateWrapper.classList.add('opacity-50');
    }

    calendar.appendChild(dateWrapper);

    iDate = new Date(iDate.setDate(iDate.getDate() + 1));
  }
}

generateCalendar();
