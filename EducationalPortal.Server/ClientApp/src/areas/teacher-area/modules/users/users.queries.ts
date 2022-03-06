import {gql} from '@apollo/client';
import {Role, User} from './users.types';
import {USER_WITH_GRADE_FRAGMENT} from './users.fragments';

export type GetUserWithGradeData = { getUser: User }
export type GetUserWithGradeVars = { id: string }

export const GET_USER_WITH_GRADE_QUERY = gql`
    ${USER_WITH_GRADE_FRAGMENT}
    query GetUser($id: ID!) {
        getUser(id: $id) {
            ...UserWithGradeFragment
        }
    }
`;

export type GetUsersWithGradeData = { getUsers: getUsersWithGradeType }
export type getUsersWithGradeType = { entities: User[], total: number }

export type GetUsersWithGradeVars = { page: number, role: Role | null }

export const GET_USERS_WITH_GRADE_QUERY = gql`
    ${USER_WITH_GRADE_FRAGMENT}
    query GetUsers($page: Int!, $role: UserRoleEnum) {
        getUsers(page: $page, role: $role) {
            entities {
                ...UserWithGradeFragment
            }
            total
        }
    }
`;
