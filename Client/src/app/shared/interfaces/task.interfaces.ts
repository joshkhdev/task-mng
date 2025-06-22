import { TaskEntityStatus } from "../../api/generated/models";

export const TaskStatusNames = {
  [TaskEntityStatus.Created]: 'Создана',
  [TaskEntityStatus.InProgress]: 'В работе',
  [TaskEntityStatus.Completed]: 'Завершена',
};
