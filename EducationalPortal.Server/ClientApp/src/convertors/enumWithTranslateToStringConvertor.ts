import {HomeworkStatus} from '../areas/teacher-area/modules/homeworks/homework.types';

export const homeworkStatusWithTranslateToString = (homeworkStatus: HomeworkStatus) => {
    switch (homeworkStatus) {
        case HomeworkStatus.New:
            return 'Нове'
        case HomeworkStatus.Approved:
            return 'Підтверджене'
        case HomeworkStatus.Unapproved:
            return 'Не прийняте'
    }
}
