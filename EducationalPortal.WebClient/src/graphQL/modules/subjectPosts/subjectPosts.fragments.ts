import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {HOMEWORK_FRAGMENT} from "../homeworks/homeworks.fragments";

// @ts-ignore
export const SUBJECT_POST_FRAGMENT = gql`
    ${USER_FRAGMENT}
    ${HOMEWORK_FRAGMENT}
    fragment SubjectPostFragment on SubjectPostType {
        id
        title
        text
        type
        teacherId
        teacher {
            ...UserFragment
        }
        homeworks @include(if: $withHomeworks){
            ...HomeworkFragment
        }
        createdAt
        updatedAt
    }
`;
