import {User} from '../users/users.types';

export type Grade = {
    id: string,
    name: string,
    students: {
        entities: User[],
        total: number,
    },
    createdAt: string,
    updatedAt: string,
}
