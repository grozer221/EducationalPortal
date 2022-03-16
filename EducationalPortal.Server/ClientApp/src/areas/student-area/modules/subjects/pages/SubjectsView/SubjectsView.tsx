import React, {useState} from 'react';
import {useQuery} from '@apollo/client';
import {Navigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {Space, Tag} from 'antd';
import Title from 'antd/es/typography/Title';
import '../../../../../../styles/table.css';
import {
    GET_SUBJECT_WITH_POSTS_QUERY,
    GetSubjectWithPostsData,
    GetSubjectWithPostsVars,
} from '../../../../../../gql/modules/subjects/subjects.queries';
import {SubjectPostsIndex} from '../../../subjectPosts/components/SubjectPostsIndex/SubjectPostsIndex';

export const SubjectsView = () => {
    const params = useParams();
    const id = params.id as string;
    const [postsPage, setPostsPage] = useState(1);
    const getSubjectQuery = useQuery<GetSubjectWithPostsData, GetSubjectWithPostsVars>(GET_SUBJECT_WITH_POSTS_QUERY,
        {variables: {id: id, postsPage: postsPage}},
    );

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getSubjectQuery.loading)
        return <Loading/>;

    const subject = getSubjectQuery.data?.getSubject;
    return (
        <Space direction={'vertical'} size={20} style={{width: '100%'}}>
            <Title level={2}>Перегляд предмету</Title>
            <Title level={3}>{subject?.name}</Title>
            <table className="infoTable">
                <tbody>
                <tr>
                    <td>Вчитель предмету:</td>
                    <td>
                        <span>{subject?.teacher.lastName} {subject?.teacher.firstName}</span>
                    </td>
                </tr>
                <tr>
                    <td>Вчителі:</td>
                    <td>
                        {subject?.teachersHaveAccessCreatePosts.map(teacher => (
                            <Tag>{teacher?.lastName} {teacher.firstName}</Tag>
                        ))}
                    </td>
                </tr>
                <tr>
                    <td>Навчальний рік:</td>
                    <td>
                        <span>{subject?.educationalYear.name}</span>
                    </td>
                </tr>
                <tr>
                    <td>Посилання:</td>
                    <td>
                        <span>{subject?.link}</span>
                    </td>
                </tr>
                <tr>
                    <td>Класи:</td>
                    <td>
                        {subject?.gradesHaveAccessRead.map(grade => <Tag>{grade?.name}</Tag>)}
                    </td>
                </tr>
                </tbody>
            </table>
            {subject &&
            <SubjectPostsIndex
                subject={subject}
                postsPage={postsPage}
                setPostsPage={setPostsPage}
            />}
        </Space>
    );
};
