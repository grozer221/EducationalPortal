import {User} from '../users/users.types';
import {Homework} from "../homeworks/homework.types";

export type SubjectPost = {
    id: string,
    title: string,
    text: string,
    type: SubjectPostType,
    teacherId: string,
    teacher: User,
    homeworks: Homework[],
    statistics: SubjectPostStatistic[],
    createdAt: string,
    updatedAt: string,
}

export enum SubjectPostType {
    Info = 'INFO',
    Homework = 'HOMEWORK',
}

export type SubjectPostStatistic = {
    key: string,
    value: number,
    hashColor: string,
}