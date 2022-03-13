import React, {FC} from 'react';
import {Card, Pagination, Row, Space} from 'antd';
import {subjectPostTypeToTag} from '../../../../../../convertors/toTagConvertor';
import Title from 'antd/es/typography/Title';
import parse from 'html-react-parser';
import {stringToUkraineDatetime} from '../../../../../../convertors/stringToDatetimeConvertors';
import {Subject} from '../../../../../teacher-area/modules/subjects/subjects.types';
import '../../../../../../styles/text.css';

type Props = {
    subject: Subject,
    postsPage: number,
    setPostsPage: (page: number) => void,
};

export const SubjectPostsIndex: FC<Props> = ({subject, postsPage, setPostsPage}) => {
    return (
        <>
            <Space direction={'vertical'} style={{width: '100%'}} size={20}>
                {subject?.posts?.entities.map(post => (
                    <Card
                        key={post.id}
                        type={'inner'}
                        title={
                            <Space size={1}>
                                {subjectPostTypeToTag(post.type)}
                                <Title level={4}>{post.title}</Title>
                            </Space>
                        }
                    >
                        <div>{parse(post.text)}</div>
                        <div className={'small'}>
                            <div>Створено: {stringToUkraineDatetime(post.createdAt)}, {post.teacher.lastName} {post.teacher.firstName}</div>
                            <div>Оновлено: {stringToUkraineDatetime(post.updatedAt)}</div>
                        </div>
                    </Card>
                ))}
                {subject?.posts?.total > 0 &&
                <Row justify={'end'}>
                    <Pagination defaultCurrent={postsPage} onChange={setPostsPage} total={subject?.posts.total}/>
                </Row>
                }
            </Space>
        </>
    );
};
