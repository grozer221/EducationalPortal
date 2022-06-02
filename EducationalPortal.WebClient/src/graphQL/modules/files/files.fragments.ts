import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';

export const FILE_FRAGMENT = gql`
    fragment FileFragment on FileType {
        id
        name
        path
        homeworkId
        createdAt
        updatedAt
    }
`;
