export function formatDateTime(date: string): string {
  return new Date(date).toLocaleString();
}

export function formatTimeSpent(date: string): string {
  // TODO: Уточнить формат
  let time = new Date(date).valueOf() / 1000; // In seconds
  const seconds = time % 60;
  time = (time - seconds) / 60; // In minutes
  const minutes = time % 60;
  time = (time - minutes) / 60; // In hours
  const hours = time % 24;
  time = (time - hours) / 24; // In days
  const days = time;

  return `${days} дней, ${hours} ч, ${minutes} мин, ${seconds} с`;
}
