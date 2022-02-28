import {gql} from '@apollo/client';
import {User} from './users.types';
import {USER_FRAGMENT} from './users.fragments';

export type GetUsersData = { getUsers: User[] }
// export type getUsers = { users: User[], total: number }

export type GetUsersVars = {}
// export type getUsersInput = {}

export const GET_USERS_QUERY = gql`
    ${USER_FRAGMENT}
    query GetUsers {
        getUsers {
            ...UserFragment
        }
    }
`;
