import {gql} from '@apollo/client';
import {GRADE_FRAGMENT} from '../grades/grades.fragments';

export const USER_FRAGMENT = gql`
    fragment UserFragment on UserType {
        id
        firstName
        lastName
        middleName
        login
        email
        phoneNumber
        dateOfBirth
        role
        gradeId
        createdAt
        updatedAt
    }
`;

export const USER_WITH_GRADE_FRAGMENT = gql`
    ${USER_FRAGMENT}
    ${GRADE_FRAGMENT}
    fragment UserWithGradeFragment on UserType {
        ...UserFragment
        grade {
            ...GradeFragment
        }
    }
`;
