import {gql} from '@apollo/client';
import {SUBJECT_FRAGMENT} from './subjects.fragments';
import {Subject} from './subjects.types';

export type GetSubjectsData = { getSubjects: getSubjectsType }
export type getSubjectsType = { entities: Subject[], total: number }

export type GetSubjectsVars = { page: number }

export const GET_SUBJECTS_QUERY = gql`
    ${SUBJECT_FRAGMENT}
    query GetSubjects($page: Int!) {
        getSubjects(page: $page) {
            entities {
                ...SubjectFragment
            }
            total
        }
    }

`;

export type GetSubjectData = { getSubject: Subject }
export type GetSubjectVars = { id: string }

export const GET_SUBJECT_QUERY = gql`
    ${SUBJECT_FRAGMENT}
    query GetSubject($id: ID!) {
        getSubject(id: $id) {
            ...SubjectFragment
        }
    }

`;
