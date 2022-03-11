import {Subject} from '../subjects/subjects.types';

export type EducationalYear = {
    id: string,
    name: string,
    dateStart: string,
    dateEnd: string,
    isCurrent: boolean,
    createdAt: string,
    updatedAt: string,
    subjects: {
        entities: Subject[],
        total: number,
    },
}
