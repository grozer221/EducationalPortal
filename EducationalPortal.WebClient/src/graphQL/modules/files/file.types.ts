import {Homework} from '../homeworks/homework.types';

export type File = {
    id: string,
    name: string,
    path: string,
    homeworkId: string,
    homework: Homework,
    createdAt: string,
    updatedAt: string,
}
