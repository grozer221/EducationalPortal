import {gql} from '@apollo/client';
import {SUBJECT_FRAGMENT} from '../subjects/subjects.fragments';
import {GRADE_FRAGMENT, GRADE_WITH_STUDENTS_FRAGMENT} from './grades.fragments';
import {Grade} from './grades.types';

export type GetGradesData = { getGrades: getGrades }
export type getGrades = { entities: Grade[], total: number }

export type GetGradesVars = { page: number }

export const GET_GRADES_QUERY = gql`
    ${GRADE_FRAGMENT}
    query GetGrades($page: Int!) {
        getGrades(page: $page) {
            entities {
                ...GradeFragment
            }
            total
        }
    }
`;

export type GetGradeData = { getGrade: Grade }
export type GetGradeVars = { id: string }

export const GET_GRADE_QUERY = gql`
    ${GRADE_FRAGMENT}
    query GetGrade($id: ID!) {
        getGrade(id: $id) {
            ...GradeFragment
        }
    }
`;

export type GetGradeWithStudentsData = { getGrade: Grade }
export type GetGradeWithStudentsVars = { id: string, studentsPage: number }

export const GET_GRADE_WITH_STUDENTS_QUERY = gql`
    ${GRADE_WITH_STUDENTS_FRAGMENT}
    query GetGrade($id: ID!, $studentsPage: Int!) {
        getGrade(id: $id) {
            ...GradeWithStudentsFragment
        }
    }
`;
