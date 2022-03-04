import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';

export const SUBJECT_POST_FRAGMENT = gql`
    ${USER_FRAGMENT}
    fragment SubjectPostFragment on SubjectPostType {
        id
        title
        text
        type
        teacherId
        teacher {
            ...UserFragment
        }
    }
`;
