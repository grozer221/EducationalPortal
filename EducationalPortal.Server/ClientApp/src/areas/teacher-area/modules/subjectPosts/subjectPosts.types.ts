import {User} from '../users/users.types';
import {EducationalYear} from '../educationalYears/educationalYears.types';

export type SubjectPost = {
    id: string,
    title: string,
    text: string,
    type: SubjectPostType,
    teacherId: string,
    teacher: User,
    createdAt: string,
    updatedAt: string,
}

export enum SubjectPostType {
    Info = 'INFO',
    Homework = 'HOMEWORK',
}
