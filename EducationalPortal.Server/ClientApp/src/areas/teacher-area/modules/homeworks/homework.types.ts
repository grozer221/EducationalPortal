import {User} from '../users/users.types';
import {EducationalYear} from '../educationalYears/educationalYears.types';
import {SubjectPost} from '../subjectPosts/subjectPosts.types';
import {Grade} from '../grades/grades.types';

export enum HomeworkStatus {
    New = 'NEW',
    Approved = 'APPROVED',
    Unapproved = 'UNAPPROVED',
}

export type Homework = {
    id: string,
    text: string,
    mark: string,
    reviewResult: string,
    status: HomeworkStatus,
    subjectPostId: string,
    subjectPost: SubjectPost,
    studentId: string,
    student: User,
    createdAt: string,
    updatedAt: string,
}
