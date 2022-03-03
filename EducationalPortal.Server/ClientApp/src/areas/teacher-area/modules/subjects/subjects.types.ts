import {User} from '../users/users.types';
import {EducationalYear} from '../educationalYears/educationalYears.types';

export type Subject = {
    id: string,
    name: string,
    link: string,
    teacherId: string,
    teacher: User,
    educationalYearId: string,
    educationalYear: EducationalYear,
    createdAt: string,
    updatedAt: string,
}
