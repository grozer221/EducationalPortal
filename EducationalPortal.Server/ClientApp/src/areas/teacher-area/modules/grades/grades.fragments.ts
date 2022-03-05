import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';

export const GRADE_FRAGMENT = gql`
    fragment GradeFragment on GradeType {
        id
        name
        createdAt
        updatedAt
    }
`;

// ${USER_FRAGMENT}
export const GRADE_WITH_STUDENTS_FRAGMENT = gql`
    ${GRADE_FRAGMENT}
    ${USER_FRAGMENT}
    fragment GradeWithStudentsFragment on GradeType {
        ...GradeFragment
        students(page: $studentsPage) {
            entities {
                ...UserFragment
            }
            total
        }
    }
`;
